if (document.getElementById('education-delete-form') !== null) {
    new Vue({
        el: '#education-delete-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
        },
        data: function () {
            return {
                errors: {
                    result: null
                },
                id: '',
                url: '/api/Education'
            };
        },
        methods: {
            submit() {
                common.delete(`${this.url}(${this.id})`)
                    .then(() => window.location.href = "/Education/Index")
                    .catch(response => this.errors.result = response.error.message);
            }
        }
    });
}