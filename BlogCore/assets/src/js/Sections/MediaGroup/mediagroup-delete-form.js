if (document.getElementById('mediagroup-delete-form') !== null) {
    new Vue({
        el: "#mediagroup-delete-form",
        data: function () {
            return {
                errors: {
                    result: null
                },
                id: null,
                loading:false,
                url: '/api/MediaGroup'
            };
        },
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
        },
        methods: {
            submit() {
                this.loading = true;
                common.delete(`${this.url}(${this.id})`)
                    .then(() => window.location.href = "/Mediagroup/Index")
                    .catch(response => { this.errors.result = response.error.message; })
                    .finally(() => this.loading = false);
            }
        }
    });
}