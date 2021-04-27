if (document.getElementById('mediagroup-management') !== null) {
    new Vue({
        el: '#mediagroup-management',
        components: {
            tableComponent
        },
        data: function () {
            return {
                options: {
                    endpoint: '/api/MediaGroup',
                    dataset: {
                        actions: {
                            details: { enabled: true, url: 'Details' },
                            edit: { enabled: true, url: 'Edit' },
                            remove: { enabled: true, url: 'Delete' }
                        },
                        showKey: false
                    },
                    key: 'Id',
                    ribbon: [{
                        icon: 'far fa-plus',
                        url:'Create'
                        }]
                }
            };
        }
    });
}