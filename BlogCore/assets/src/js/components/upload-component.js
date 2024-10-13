// upload-component
export const uploadComponent = defineComponent({
    data: function () {
        return {
            url: null,
            type: /image\/*/i
        };
    },
    methods: {
        addFile(e) {
            let file = e.dataTransfer.files[0];
            if (file.type.match(this.type)) {
                this.handleFile(file);
            }
        },
        clear() {
            this.$emit('update:modelValue', null);
        },
        handleFile(file) {
            this.url = URL.createObjectURL(file);
            this.$emit('update:modelValue', file);
        },
        handleFileChange(e) {
            const file = e.target.files[0];
            this.handleFile(file);
        },
        toKb(val) {
            return Math.floor(val / 1024);
        }
    },
    props: {
        modelValue: File
    },
    emits: ['update:modelValue'],
    template: `
        <label class="file-upload" v-on:drop.prevent="addFile" v-on:dragover.prevent>
            <div v-if="modelValue" class="file-preview-window">
                <div class="thumbnail">
                    <img v-bind:src="url"/>
                </div>
                <div class="file-info-box">
                    <div class="header">
                        Selected File
                    </div>
                    <div class="body">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Type</th>
                                    <th>Size</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>{{modelValue.name}}</td>
                                    <td>{{modelValue.type}}</td>
                                    <td>{{toKb(modelValue.size)}} kb</td>
                                    <td><i class="fas fa-trash-alt fa-fw clickable" v-on:click.prevent="clear" title="clear media"></i></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="placeholder" v-else>
                <div class="header">
                    <i class="fas fa-upload fa-2x"></i>
                </div>
                <div class="body">
                    Drop or click to select file
                </div>
            </div>
            <input class="d-none" type="file" v-on:change="handleFileChange" accept="image/*" />
        </label>
    `
});
