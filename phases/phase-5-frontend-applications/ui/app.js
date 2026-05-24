const fallbackBackendUrl = 'http://localhost:5055';
const backendUrl = window.location.protocol === 'file:'
    ? fallbackBackendUrl
    : window.location.origin;

const statusEl = document.getElementById('backendStatus');
const urlEl = document.getElementById('backendUrl');
const readinessEl = document.getElementById('readinessContent');
const organizationList = document.getElementById('organizationList');
const memberList = document.getElementById('memberList');
const organizationSelect = document.getElementById('organizationSelect');
const diagramEl = document.getElementById('diagramCanvas');
const diagramSummaryEl = document.getElementById('diagramSummary');
const actionStatusEl = document.getElementById('actionStatus');

urlEl.textContent = backendUrl;

function apiUrl(path) {
    return `${backendUrl}${path}`;
}

function getValue(source, primaryKey, fallbackKey) {
    return source?.[primaryKey] ?? source?.[fallbackKey] ?? '';
}

function hasNetwork(network) {
    return Boolean(network
        && (getValue(network, 'networkName', 'NetworkName')
            || getValue(network, 'channelName', 'ChannelName')
            || getValue(network, 'environment', 'Environment')));
}

function renderEmpty(target, message) {
    target.innerHTML = `<p class="empty-state">${message}</p>`;
}

function getMembersByOrganization(members) {
    return members.reduce((accumulator, member) => {
        const organizationId = getValue(member, 'organizationId', 'OrganizationId');
        accumulator[organizationId] = (accumulator[organizationId] ?? 0) + 1;
        return accumulator;
    }, {});
}

function setActionStatus(message, tone = 'default') {
    actionStatusEl.textContent = message;
    actionStatusEl.className = 'action-status';

    if (tone === 'success') {
        actionStatusEl.classList.add('success');
    }

    if (tone === 'error') {
        actionStatusEl.classList.add('error');
    }
}

function renderReadiness(network, organizations, members) {
    if (!hasNetwork(network)) {
        readinessEl.innerHTML = '<p class="empty-state">Create a network first to see the readiness plan.</p>';
        return;
    }

    const networkName = getValue(network, 'networkName', 'NetworkName');
    const environment = getValue(network, 'environment', 'Environment');
    const channelName = getValue(network, 'channelName', 'ChannelName');
    const ordererHost = getValue(network, 'ordererHost', 'OrdererHost');

    const steps = [
        { title: 'Network scaffolded', detail: `${networkName} is configured for ${environment}.` },
        { title: 'Channel prepared', detail: `The demo will target ${channelName} on ${ordererHost}.` },
        { title: 'Organizations onboarded', detail: `${organizations.length} organization(s) are ready for governance.` },
        { title: 'Members assigned', detail: `${members.length} member(s) have been added to the demo environment.` },
        { title: 'Backend connection verified', detail: 'The UI is connected to the backend prototype and ready for a live demo.' }
    ];

    readinessEl.innerHTML = steps.map((step) => `
    <div class="readiness-step">
      <strong>${step.title}</strong>
      <span>${step.detail}</span>
    </div>
  `).join('');
}

function renderOrganizations(organizations) {
    if (!organizations.length) {
        renderEmpty(organizationList, 'No organizations created yet.');
        organizationSelect.innerHTML = '<option value="">No organizations yet</option>';
        return;
    }

    organizationSelect.innerHTML = organizations
        .map((org) => `<option value="${getValue(org, 'organizationId', 'OrganizationId')}">${getValue(org, 'organizationName', 'OrganizationName')}</option>`)
        .join('');

    organizationList.innerHTML = organizations.map((org) => `
    <div class="list-item">
      <strong>${getValue(org, 'organizationName', 'OrganizationName')}</strong>
      <span>${getValue(org, 'organizationType', 'OrganizationType')} • Admin: ${getValue(org, 'adminName', 'AdminName')} • ${getValue(org, 'readiness', 'Readiness')}</span>
    </div>
  `).join('');
}

function renderMembers(members) {
    if (!members.length) {
        renderEmpty(memberList, 'No members added yet.');
        return;
    }

    memberList.innerHTML = members.map((member) => `
    <div class="list-item">
      <strong>${getValue(member, 'memberName', 'MemberName')}</strong>
      <span>${getValue(member, 'memberRole', 'MemberRole')} • ${getValue(member, 'memberEmail', 'MemberEmail')} • ${getValue(member, 'status', 'Status')}</span>
    </div>
  `).join('');
}

function renderDiagramSummary(network, organizations, members) {
    if (!hasNetwork(network)) {
        diagramSummaryEl.innerHTML = '';
        return;
    }

    const totalMembers = members.length;

    diagramSummaryEl.innerHTML = `
    <div class="metric-card">
      <span class="metric-label">Network</span>
      <strong>${getValue(network, 'networkName', 'NetworkName')}</strong>
    </div>
    <div class="metric-card">
      <span class="metric-label">Channel</span>
      <strong>${getValue(network, 'channelName', 'ChannelName')}</strong>
    </div>
    <div class="metric-card">
      <span class="metric-label">Organizations</span>
      <strong>${organizations.length}</strong>
    </div>
    <div class="metric-card">
      <span class="metric-label">Members</span>
      <strong>${totalMembers}</strong>
    </div>
  `;
}

function renderDiagram(network, organizations, members) {
    if (!hasNetwork(network)) {
        diagramEl.innerHTML = '<p class="empty-state">Create a network to see the visual diagram.</p>';
        diagramSummaryEl.innerHTML = '';
        return;
    }

    const memberCounts = getMembersByOrganization(members);

    if (!organizations.length) {
        diagramEl.innerHTML = `
      <div class="diagram-empty-state">
        <strong>No organizations yet</strong>
        <span>Add an organization to visualize how the network connects to membership and channel governance.</span>
      </div>
    `;
        renderDiagramSummary(network, organizations, members);
        return;
    }

    const startX = 180;
    const spacing = organizations.length === 1 ? 0 : 640 / (organizations.length - 1);

    const lines = organizations.map((_, index) => {
        const orgX = Math.round(startX + (index * spacing));
        return `<path d="M 500 98 C 500 130, ${orgX} 130, ${orgX} 180" fill="none" stroke="rgba(99, 239, 255, 0.65)" stroke-width="3" stroke-linecap="round" stroke-dasharray="7 10" />`;
    }).join('');

    const orgNodes = organizations.map((organization, index) => {
        const memberCount = memberCounts[getValue(organization, 'organizationId', 'OrganizationId')] ?? 0;
        const orgX = Math.round(startX + (index * spacing));

        return `
      <div class="diagram-node diagram-org" style="left:${orgX}px; top:170px;">
        <span class="node-label">Organization</span>
        <strong>${getValue(organization, 'organizationName', 'OrganizationName')}</strong>
        <span>${getValue(organization, 'organizationType', 'OrganizationType')}</span>
        <span class="diagram-member-pill">${memberCount} member${memberCount === 1 ? '' : 's'}</span>
      </div>
    `;
    }).join('');

    diagramEl.innerHTML = `
    <svg class="diagram-svg" viewBox="0 0 1000 320" preserveAspectRatio="none">
      <defs>
        <linearGradient id="diagramGlow" x1="0%" y1="0%" x2="100%" y2="100%">
          <stop offset="0%" stop-color="rgba(99, 239, 255, 0.85)" />
          <stop offset="100%" stop-color="rgba(122, 140, 255, 0.65)" />
        </linearGradient>
      </defs>
      ${lines}
      <circle cx="500" cy="98" r="10" fill="url(#diagramGlow)" />
    </svg>
    <div class="diagram-node diagram-network">
      <span class="node-label">Network</span>
      <strong>${getValue(network, 'networkName', 'NetworkName')}</strong>
      <span>${getValue(network, 'channelName', 'ChannelName')}</span>
      <span>${getValue(network, 'peerCount', 'PeerCount')} peer(s) • ${getValue(network, 'ordererHost', 'OrdererHost')}</span>
    </div>
    ${orgNodes}
  `;

    renderDiagramSummary(network, organizations, members);
}

async function loadDashboard() {
    try {
        const [status, network, organizations, members] = await Promise.all([
            fetch(apiUrl('/')).then((response) => response.json()),
            fetch(apiUrl('/infrastructure/network')).then((response) => response.ok ? response.json() : null),
            fetch(apiUrl('/infrastructure/organizations')).then((response) => response.ok ? response.json() : []),
            fetch(apiUrl('/infrastructure/members')).then((response) => response.ok ? response.json() : [])
        ]);

        statusEl.textContent = `${status.service} • ${status.status}`;
        renderReadiness(network, organizations, members);
        renderOrganizations(organizations);
        renderMembers(members);
        renderDiagram(network, organizations, members);
    } catch (error) {
        statusEl.textContent = 'Backend unavailable';
        statusEl.style.color = '#ff8aa2';
        renderReadiness(null, [], []);
        renderOrganizations([]);
        renderMembers([]);
        renderDiagram(null, [], []);
        console.error(error);
    }
}

async function createNetwork(event) {
    event.preventDefault();

    const payload = {
        networkName: document.getElementById('networkName').value,
        channelName: document.getElementById('channelName').value,
        ordererHost: document.getElementById('ordererHost').value,
        peerCount: Number(document.getElementById('peerCount').value),
        environment: document.getElementById('environment').value
    };

    try {
        const response = await fetch(apiUrl('/infrastructure/network'), {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            throw new Error('Network save failed');
        }

        const savedNetwork = await response.json();
        setActionStatus(`Network saved: ${getValue(savedNetwork, 'networkName', 'NetworkName')} -> ${getValue(savedNetwork, 'channelName', 'ChannelName')}`, 'success');
        await loadDashboard();
    } catch (error) {
        setActionStatus(error instanceof Error ? error.message : 'Failed to save network', 'error');
    }
}

async function createOrganization(event) {
    event.preventDefault();

    const payload = {
        organizationName: document.getElementById('organizationName').value,
        organizationType: document.getElementById('organizationType').value,
        adminName: document.getElementById('adminName').value,
        adminEmail: document.getElementById('adminEmail').value
    };

    try {
        const response = await fetch(apiUrl('/infrastructure/organizations'), {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            throw new Error('Organization save failed');
        }

        const savedOrganization = await response.json();
        setActionStatus(`Organization saved: ${getValue(savedOrganization, 'organizationName', 'OrganizationName')}`, 'success');
        await loadDashboard();
    } catch (error) {
        setActionStatus(error instanceof Error ? error.message : 'Failed to save organization', 'error');
    }
}

async function addMember(event) {
    event.preventDefault();

    if (!organizationSelect.value) {
        setActionStatus('Choose an organization before adding a member.', 'error');
        return;
    }

    const payload = {
        organizationId: organizationSelect.value,
        memberName: document.getElementById('memberName').value,
        memberRole: document.getElementById('memberRole').value,
        memberEmail: document.getElementById('memberEmail').value
    };

    try {
        const response = await fetch(apiUrl('/infrastructure/members'), {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            throw new Error('Member save failed');
        }

        const savedMember = await response.json();
        setActionStatus(`Member saved: ${getValue(savedMember, 'memberName', 'MemberName')}`, 'success');
        await loadDashboard();
    } catch (error) {
        setActionStatus(error instanceof Error ? error.message : 'Failed to save member', 'error');
    }
}

document.getElementById('networkForm').addEventListener('submit', createNetwork);
document.getElementById('orgForm').addEventListener('submit', createOrganization);
document.getElementById('memberForm').addEventListener('submit', addMember);

loadDashboard();
