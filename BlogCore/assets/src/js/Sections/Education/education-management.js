if (document.getElementById('education-management') !== null) {
    new Vue({
        el: '#education-management',
        components: {
            tableComponent
        },
        data: function () {
            return {
                options: {
                    endpoint: '/api/Education',
                    dataset: {
                        actions: {
                            details: { enabled: true, url: 'Details' },
                            edit: { enabled: true, url: 'Edit' },
                            remove: { enabled: true, url: 'Delete' }
                        },
                        hide:['Id', 'ImageUrl']
                    },
                    key:'Id',
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