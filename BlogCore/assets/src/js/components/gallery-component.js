// gallery-component
export const galleryComponent = defineComponent({
    created() {
        this.parseOptions();
        common.get(this.getUrl())
            .then(response => {
                let groups = {};
                response.value.forEach(medialink => {
                    if (this.gallery_options.grouping.enabled) {
                        const groupEntity = this.gallery_options.grouping.entity;
                        let group = medialink[groupEntity];
                        delete medialink[groupEntity];

                        if (groups[group.NormalizedName]) {
                            groups[group.NormalizedName].data.push(medialink);
                        }
                        else {
                            groups[group.NormalizedName] = {
                                Name: group.Name,
                                NormalizedName: group.NormalizedName,
                                data: [medialink]
                            };
                        }
                    }
                    else {
                        this.data.push(medialink);
                    }
                });

                if (this.gallery_options.grouping.enabled) {
                    for (let group in groups) {
                        this.data.push({
                            Name: groups[group].Name,
                            NormalizedName: groups[group].NormalizedName,
                            data: groups[group].data
                        });
                    }
                }

                this.success = true;

            })
            .catch(error => console.error(error));
    },
    data() {
        return {
            data: [],
            gallery_options: {
                actions: {
                    remove: { enabled: false, url: '' }
                },
                endpoint: '',
                grouping: {
                    enabled: false,
                    entity: null
                },
                key: '',
                ribbon: []//format {icon:'fas fa-plus', url:'http://domain.com/action'}
            },
            loading: false,
            open: false,
            selected: null,
            selectedImage: null,
            success: false
        };
    },
    methods: {
        clearSelection() {
            this.selected = null;
        },
        close() {
            this.selectedImage = null;
            this.open = false;
        },
        display(val) {
            this.loading = true;
            let image = new Image();

            this.selectedImage = val;
            image.src = val.Url;
            image.onload = () => {
                this.loading = false;
            };
            this.open = true;
        },
        expand(base, value) {
            let obj = {};
            for (const key in base) {
                if (value[key] !== undefined) {
                    if (typeof value[key] === "object" && !Array.isArray(value[key])) {
                        obj[key] = this.expand(base[key], value[key]);
                    }
                    else {
                        obj[key] = value[key];
                    }
                } else {
                    obj[key] = base[key];
                }
            }

            return obj;
        },
        parseOptions() {
            this.gallery_options = this.expand(this.gallery_options, this.options);
        },
        getUrl() {
            if (this.gallery_options.grouping.enabled) {
                return `${this.gallery_options.endpoint}?$expand=${this.gallery_options.grouping.entity}`;
            }
            else {
                return this.gallery_options.endpoint;
            }
        },
        select(val) {
            this.selected = val;
        }
    },
    props: {
        options: {
            required: true,
            type: Object
        }
    },
    template: `
        <div v-if="success" class="gallery-component">
                <div class="table-ribbon">
                    <a v-for="action in gallery_options.ribbon" v-bind:href="action.url" class="item">
                        <i class="fa-fw" v-bind:class="action.icon"></i>
                    </a>
                </div>
                <div class="body">
                    <div v-if="selected != null" class="gallery-container" >
                        <div class="gallery-image clickable -primary" v-on:click="clearSelection">
                            <div class="button" >
                                <div><i class="far fa-arrow-up fa-fw fa-3x"></i></div>
                            </div>
                        </div>
                        <div v-for="image in data[selected].data" class="gallery-image clickable" >
                            <a :href="'delete/'+image.Id" class="fa-stack clickable remove">
                              <i class="fas fa-square fa-stack-2x"></i>
                              <i class="fas fa-trash-alt fa-stack-1x fa-inverse"></i>
                            </a>
                            <div class="child" v-bind:style="{backgroundImage:'url('+image.Thumbnail+')'}" v-on:click="display(image)">
                            </div>
                        </div>
                    </div>
                    <div v-else   class="gallery-container">
                        <div v-for="(group,index) in data" class="group-thumbnail" v-on:click="select(index)">
                            <div class="body">
                                {{group.data.length}}
                            </div>
                            <div class="title">
                               <i class="far fa-folder fa-fw"></i> {{group.Name}}
                            </div>
                        </div>
                    </div>
                    <div v-if="data.length===0" class="empty-panel">
                        <div class="header">
                            <i class="far fa-info-circle fa-fw fa-2x"></i>
                        </div>
                        <div class="body">
                            There is no data to show.
                        </div>
                    </div>
                </div>
                <transition name="modal-fade">
                    <div v-if="selectedImage" class="overlay" v-bind:class="{'d-none':!open}">
                        <div class="image-container">
                            <div class="inner">                         
                                <div class="header">
                                    {{selectedImage.Name}}
                                    <i class="far fa-times fa-fw clickable" v-on:click="close"></i>
                                </div>
                                <div class="body">
                                    <i v-if="loading" class="fas fa-spinner fa-pulse fa-2x"></i>
                                    <img v-bind:src="selectedImage.Url"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </transition>
        </div>
        <div v-else class="loading-panel">
            <i class="fas fa-sync fa-spin fa-fw fa-2x"></i>
        </div>
    `
});
