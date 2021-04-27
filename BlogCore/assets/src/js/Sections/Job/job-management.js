if (document.getElementById('job-management') !== null) {
    new Vue({
        el: '#job-management',
        components: {
            tableComponent
        },
        data: function () {
            return {
                options: {
                    endpoint: '/api/Job',
                    dataset: {
                        actions: {
                            details: { enabled: true, url: 'Details' },
                            edit: { enabled: true, url: 'Edit' },
                            remove: { enabled: true, url: 'Delete' }
                        },
                        hide:['Id','Description']
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