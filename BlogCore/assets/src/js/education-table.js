if (document.getElementById('education-table') !== null) {
    new Vue({
        el: '#education-table',
        components: {
            tableComponent: window.tableComponent
        },
        data: function () {
            return {
                endpoint: 'https://localhost:44396/api/Education',
                data: [],
                ribbon: [{
                    url: 'Create',
                    icon:'far fa-plus'
                }],
                schema: [],
                success: true,
                test: [{}]
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