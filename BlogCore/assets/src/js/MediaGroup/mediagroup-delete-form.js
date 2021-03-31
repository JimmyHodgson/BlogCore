if (document.getElementById('mediagroup-delete-form') !== null) {
    new Vue({
        el: "#mediagroup-delete-form",
        data: function () {
            return {
                errors: {
                    result: null
                },
                url: '/api/MediaGroup'
            };
        },
        methods: {
            submit() {
                if (this.validateName()) {
                    common.delete(this.url)
                        .then(() => window.location.href = "Index")
                        .catch(response => { this.errors.result = response.error.message; });
                }
            }
        }
    });
}