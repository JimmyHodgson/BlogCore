if (document.getElementById('achievement-management') !== null) {
    new Vue({
        el: '#achievement-management',
        components: {
            tableComponent
        },
        data: function () {
            return {
                options: {
                    endpoint: 'https://localhost:44396/api/Achievement',
                    dataset: {
                        actions: {
                            details: { enabled: true, url: 'Details' },
                            edit: { enabled: true, url: 'Edit' },
                            remove: { enabled: true, url: 'Delete' }
                        },
                        hide:['Id']
                    },
                    key: 'Id',
                    ribbon: [{
                        url: 'Create',
                        icon: 'far fa-plus'
                    }]
                }
            };
        },
        mounted: function () {
        },
        methods: {
            isEmpty() {
                return !this.data.length > 0;
            }
        }
    });
}