if (document.getElementById('education-table') !== null) {
    new Vue({
        el: '#education-table',
        data: function () {
            return {
                endpoint: '',
                data: [],
                success:true
            };
        },
        methods: {
            isEmpty() {
                return !this.data.length > 0;
            }
        }
    });
}