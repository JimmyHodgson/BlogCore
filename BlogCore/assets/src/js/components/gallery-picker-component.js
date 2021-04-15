const galleryPickerComponent = Vue.component('gallery-picker-component', {
    data: function () {
        return {
            data: {},
            selected: null,
            show: false
        };
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
            val = val === undefined ? this.$refs.galleryInput.value : val;

            this.$emit('input', val);
        }
    },
    props: {
        source: {
            required: true,
            type: Array
        },
        value: {
            required: true,
            type: String
        }
    },
    template:
        `   <div class="gallery-picker-component" v-click-outside="close">
                <div class="input-group clickable" >
                    <div class="input-group-prepend" v-on:click="toggleOpen">
                        <span class="input-group-text -primary">
                            <i class="fas fa-image fa-fw"></i>
                        </span>
                    </div>
                    <input type="text" ref="galleryInput" class="form-control" :value="value" v-on:input="update()"  disabled/>
                    <div v-if="value.length !== 0" class="input-group-append" v-on:click="removeSelection">
                        <span class="input-group-text -danger">
                            <i class="fal fa-times fa-fw"></i>
                        </span>
                    </div>
                </div>
                <div class="gallery-picker" :class="{'-open':show,'-hide':!show}">
                    <div v-for="(value,key) in data" class="gallery-picker-group" :class="{'-selected':selected===key}">
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