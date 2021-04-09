if (document.getElementById('medialink-management') !== null) {
    new Vue({
        el: '#medialink-management',
        components: {
            galleryComponent
        },
        data: function () {
            return {
                options: {
                    endpoint: 'https://localhost:44396/api/MediaLink',
                    grouping: {
                        enabled: true,
                        entity: 'Group'
                    },
                    key: 'Id',
                    ribbon: [{
                        icon: 'far fa-plus',
                        url: 'Create'
                    }]
                }
            };
        }
    });
}