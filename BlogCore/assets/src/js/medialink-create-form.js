if (document.getElementById('medialink-create-form') !== null) {
    new Vue({
        el: "#medialink-create-form",
        components: {
            uploadComponent: window.uploadComponent
        },
        data: function () {
            return {
                file: null,
                name: '',
                bucket:'default'
            };
        },
        methods: {
            
        },
        mounted() {
            
        },
        watch: {
            file: function (newFile) {
                if (newFile !== null) this.name = newFile.name;
                else this.name = '';
            }
        }
    });
}