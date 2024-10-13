if (document.getElementById('medialink-create-form') !== null) {
    Promise.all([
        import('vue'),
        import('components'),
        import('common')
    ]).then(([{ createApp }, { uploadComponent }, { common }]) => {
        createApp({
            components: {
                uploadComponent
            },
            beforeMount() {
                common.get(`${this.url_groups}?$orderby=Name`).then(data => {
                    this.groups = data.value;
                    if (this.groups.length > 0) {
                        this.selected = this.groups[0].Id;
                    }
                    else {
                        this.errors.group = 'No media groups available.';
                    }
                }).catch(error => console.error(error));
            },
            data: function () {
                return {
                    errors: {
                        file: null,
                        group: null,
                        name: null,
                        result: null
                    },
                    extRegex: /\.[^ /.]+$/,
                    file: null,
                    groups: [],
                    loading: false,
                    name: '',
                    nameRegex: /^[A-Za-z0-9_]*$/,
                    selected: '',
                    url: '/api/MediaLink',
                    url_groups: '/api/MediaGroup'
                };
            },
            methods: {
                checkForm() {
                    let name = this.validateName();
                    let file = this.validateFile();
                    let group = this.validateGroup();
                    return name && file && group;
                },
                submit() {
                    if (this.checkForm()) {
                        this.loading = true;
                        const fd = new FormData();
                        fd.append('file', this.file);
                        fd.append('name', `${this.name}.${this.getType(this.file.type)}`);
                        fd.append('group', this.selected);
                        common.postMultiPart(this.url, fd)
                            .then(() => window.location.href = "Index")
                            .catch(response => {
                                this.errors.result = response.error.message;
                            })
                            .finally(() => this.loading = false);
                    }
                },
                getGroup() {
                    this.groups.forEach(group => {
                        if (group.Id === this.selected)
                            return group;
                    });
                    return null;
                },
                getType(val) {
                    return val.slice(6, val.length);
                },
                validateGroup() {
                    if (this.groups.length > 0) {
                        return true;
                    }
                    return false;
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
            watch: {
                file: function (newFile) {
                    if (newFile !== null) {
                        this.name = newFile.name.replace(this.extRegex, '');
                    }
                    else this.name = '';
                    this.validateFile();
                },
                name: function () {
                    this.validateName();
                }
            }
        }).mount("#medialink-create-form");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}
