if (document.getElementById('medialink-create-form') !== null) {
    new Vue({
        el: "#medialink-create-form",
        components: {
            uploadComponent: window.uploadComponent
        },
        data: function () {
            return {
                bucket: 'default',
                errors: {
                    file: null,
                    name: null
                },
                extRegex: /\.[^ /.]+$/,
                file: null,
                loading:false,
                name: '',
                nameRegex: /^[A-Za-z0-9_]*$/,
                url: '/api/MediaLink',
            };
        },
        methods: {
            checkForm() {
                let name = this.validateName();
                let file = this.validateFile();
                return name && file;
            },
            submit() {
                if (this.checkForm()) {
                    this.loading = true;
                    const fd = new FormData();
                    fd.append('file', this.file);
                    fd.append('name', this.name);
                    fd.append('bucket', this.bucket);
                    common.postMultiPart(this.url, fd)
                        .then(response => {
                            console.log(response);
                        })
                        .catch(error => console.error(error))
                        .finally(() => this.loading = false);
                }
            },
            getType(val) {
                return val.slice(6, val.length);
            },
            validateName() {
                if (this.name.trim() === '') {
                    this.errors.name = 'Name is required.';
                    return false;
                }
                else if (!this.name.match(this.nameRegex)) {
                    this.errors.name = 'Name can only consist of letters, numbers and underscore';
                    return false;
                }
                else {
                    this.errors.name = null;
                }
                return true;
            },
            validateFile() {
                if (this.file === null) {
                    this.errors.file = 'Please select an image to upload';
                    return false;
                } else {
                    this.errors.file = null;
                }
                return true;
            }
        },
        mounted() {

        },
        watch: {
            file: function (newFile) {
                if (newFile !== null) {
                    this.name = newFile.name.replace(this.extRegex,'');
                }
                else this.name = '';
                this.validateFile();
            },
            name: function () {
                this.validateName();
            }
        }
    });
}