if (document.getElementById('mediagroup-management') !== null) {
    new Vue({
        el: '#mediagroup-management',
        components: {
            tableComponent
        },
        data: function () {
            return {
                endpoint: 'https://localhost:44396/api/MediaGroup',
                data: [],
                ribbon: [{
                    url: 'Create',
                    icon: 'far fa-plus'
                }],
                schema: [],
                success: true,
                test: [{}],
                options: {
                    endpoint: 'https://localhost:44396/api/MediaGroup',
                    dataset: {
                        showKey: false,
                        actions: {
                            edit: { enabled: true, url: 'Edit' },
                            details: { enabled: true, url: 'Details' },
                            remove: { enabled: true, url: 'Delete' }
                        }
                    },
                    key: 'Id',
                    ribbon: [{
                        icon: 'far fa-plus',
                        url:'Create'
                        }]
                }
            };
        },
        mounted: function () {
        },
        methods: {

        }
    });
}