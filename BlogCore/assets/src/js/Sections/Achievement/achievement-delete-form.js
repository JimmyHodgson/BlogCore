if (document.getElementById('achievement-delete-form') !== null) {
    new Vue({
        el: '#achievement-delete-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
        },
        data: function () {
            return {
                errors: {
                    result: null
                },
                id: '',
                url: '/api/Achievement'
            };
        },
        methods: {
            submit() {
                common.delete(`${this.url}(${this.id})`)
                    .then(() => window.location.href = "/Achievement/Index")
                    .catch(response => this.errors.result = response.error.message);
            }
        }
    });
}