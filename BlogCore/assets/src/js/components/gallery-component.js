const galleryComponent = Vue.component('gallery-component', {
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
            selected: null,
            success: false
        };
    },
    methods: {
        clearSelection() {
            this.selected = null;
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
                        <div class="gallery-image">
                            <div class="button clickable" v-on:click="clearSelection">
                                <div><i class="far fa-arrow-up fa-fw fa-3x"></i></div>
                            </div>
                        </div>
                        <div v-for="image in data[selected].data" class="gallery-image clickable">
                            <div class="child" v-bind:style="{backgroundImage:'url('+image.Url+')'}">
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
        </div>
        <div v-else class="loading-panel">
            <i class="fas fa-sync fa-spin fa-fw fa-2x"></i>
        </div>
    `
});