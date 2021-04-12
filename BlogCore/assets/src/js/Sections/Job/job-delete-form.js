if (document.getElementById('job-delete-form') !== null) {
    new Vue({
        el: '#job-delete-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
        },
        data: function () {
            return {
                errors: {
                    result: null
                },
                id: '',
                url: '/api/Job'
            };
        },
        methods: {
            submit() {
                common.delete(`${this.url}(${this.id})`)
                    .then(() => window.location.href = "/Job/Index")
                    .catch(response => this.errors.result = response.error.message);
            }
        }
    });
}