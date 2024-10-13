// gallery-picker-component
export const galleryPickerComponent = defineComponent({
    data: function () {
        return {
            data: {},
            selected: null,
            show: false
        };
    },
    directives: {
        'click-outside': clickOutsideDirective
    },
    methods: {
        close() {
            this.show = false;
        },
        getHeight(val) {
            if (this.selected !== val) {
                return '0px';
            }
            else {
                return `${this.$refs[val][0].scrollHeight}px`;
            }
        },
        pick(val) {
            this.update(val.Url);
            this.close();
        },
        removeSelection() {
            this.update('');
        },
        selectGroup(val) {
            if (this.selected === val) {
                this.selected = null;
            }
            else {
                this.selected = val;
            }
        },
        toggleOpen() {
            this.show = !this.show;
        },
        update(val) {
            if ( val === undefined ) {
                val = this.$refs.galleryInput.modelValue;
            }
            else {
                this.$emit('update:modelValue', val);
            }
        }
    },
    props: {
        source: {
            required: true,
            type: Array
        },
        modelValue: {
            required: true,
            type: String
        }
    },
    emits: ['update:modelValue'],
    template:
        `   <div class="gallery-picker-component" v-click-outside="close">
                <div class="input-group clickable" >
                    <div class="input-group-text -primary" v-on:click="toggleOpen">
                        <i class="fas fa-image fa-fw"></i>
                    </div>
                    <input type="text" ref="galleryInput" class="form-control" :value="modelValue" v-on:input="update()"  disabled/>
                    <div v-if="modelValue.length !== 0" class="input-group-text -danger" v-on:click="removeSelection">
                        <i class="fal fa-times fa-fw"></i>
                    </div>
                </div>
                <div class="gallery-picker" :class="{'-open':show,'-hide':!show}">
                    <div v-if="Object.keys(data).length === 0" class="gallery-picker-empty">
                        <div class="header">
                                <div>Gallery</div>
                                <div v-on:click="toggleOpen"><i class="fal fa-times fa-fw clickable"></i></div>
                        </div>
                        <div class="body">
                            <i class="fas fa-exclamation-circle fa-2x"></i>
                            <p>No items to display</p>
                        </div>
                    </div>
                    <div v-else v-for="(value,key) in data" class="gallery-picker-group" :class="{'-selected':selected===key}">
                        <div class="header clickable" v-on:click="selectGroup(key)">
                            {{key}}
                        </div>
                        <div  class="body" :ref="key" :style="{height:getHeight(key)}">
                            <div v-on:click="pick(image)" v-for="image in value" class="thumbnail clickable" :style="{backgroundImage:'Url('+image.Thumbnail+')'}">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `,
    watch: {
        show: function () {
            if (this.show === false) {
                this.selected = null;
            }
        },
        source: function () {
            this.data = {};
            this.source.forEach(image => {
                let group = image.Group.Name;
                if (!this.data[group]) {
                    this.data[group] = [];
                }
                this.data[group].push(image);
            });
        }
    }
});
