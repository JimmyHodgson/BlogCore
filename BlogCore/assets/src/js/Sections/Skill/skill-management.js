if (document.getElementById('skill-management') !== null) {
    new Vue({
        el: '#skill-management',
        components: {
            tableComponent
        },
        data: function () {
            return {
                options: {
                    endpoint: '/api/Skill',
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