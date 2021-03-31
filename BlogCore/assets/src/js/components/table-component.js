const tableComponent = Vue.component('table-component', {
    data: function () {
        return {
            count: 0,
            data: [],
            page: 0,
            headers: [],
            order: null,
            performance: "0 ms",
            success: false,
            table_options: {
                dataset: {
                    showKey: true,
                    actions: {
                        edit: { enabled: false, url: '' },
                        details: { enabled: false, url: '' },
                        remove: { enabled: false, url: '' }
                    }
                },
                endpoint: null,
                key: '',
                order: null,
                pageSize: 10,
                ribbon: []//format {icon:'fas fa-plus', url:'http://domain.com/action'}
            }
        };
    },
    computed: {
        totalPages: function () {
            return parseInt(Math.ceil(this.count / this.table_options.pageSize));
        },
        hasNextPage: function () {
            return this.page < this.totalPages;
        },
        hasPrevPage: function () {
            return this.page > 1;
        }
    },
    methods: {
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
        format(data) {
            //TODO 
            /*
             * This function is to possibly be able to format data based on
             * a schema provided in the options property
             * For example:
             * 1. Image links could be displayed as images.
             * 2. Text could be displayed as a badge.
             * 3. Possible conditional formatting.
             */
            return data;
        },
        getData() {
            let start = performance.now();
            let end;
            common.get(`${this.table_options.endpoint}?$top=${this.table_options.pageSize}&$skip=${(this.page - 1) * this.table_options.pageSize}${this.getOrderString()}`)
                .then(data => {
                    end = performance.now();
                    this.performance = `${(end - start).toFixed(0)} ms`;
                    this.data = this.format(data.value);
                })
                .catch(error => {
                    console.error(error);
                });
        },
        getHeaders() {
            let headers = [];
            if (this.data.length > 0) {
                for (let key in this.data[0]) {
                    if (key !== this.table_options.key || this.table_options.dataset.showKey) {
                        headers.push(key);
                    }
                }
            }
            this.headers = headers;
        },
        getHeaderPercentage() {
            return 100 / this.headers.length;
        },
        getOrderString() {
            if (this.order) {
                if (this.order.direction === 1) {
                    return `&$orderby=${this.order.header}`;
                }
                else {
                    return `&$orderby=${this.order.header} desc`;
                }
            }
            return '';
        },
        nextPage() {
            this.page += 1;
            this.getData();
        },
        pageEnd() {
            let brutePageEnd = this.table_options.pageSize * this.page;
            return brutePageEnd > this.count ? this.count : brutePageEnd;
        },
        pageStart() {
            let brutePageStart = (this.page - 1) * this.table_options.pageSize + 1;
            return this.count < brutePageStart ? this.count : brutePageStart;
        },
        parseOptions() {
            this.table_options = this.expand(this.table_options, this.options);
        },
        prevPage() {
            this.page -= 1;
            this.getData();
        },
        updateOrder(value) {
            if (this.order && this.order.header === value) {
                if (this.order.direction + 1 > 2) {
                    this.order = null;
                }
                else {
                    this.order.direction += 1;
                }
            }
            else {
                this.order = {
                    header: value,
                    direction: 1
                };
            }

            this.getData();
        }
    },
    created() {
        this.parseOptions();
        let start = performance.now();
        let end;
        common.get(`${this.table_options.endpoint}?$count=true&$top=${this.table_options.pageSize}${this.getOrderString()}`)
            .then(data => {
                end = performance.now();
                this.performance = `${(end - start).toFixed(0)} ms`;
                this.count = data['@odata.count'];
                if (data.value.length > 0) {
                    this.page = 1;
                    this.data = this.format(data.value);
                }
                this.getHeaders();
                this.success = true;
            })
            .catch(error => {
                console.error(error);
            });
    },
    props: {
        options: {
            required: true,
            type: Object
        }
    },
    template:
        `
            <div v-if="success" class="table-component">
                <div class="table-ribbon">
                    <a v-for="action in table_options.ribbon" v-bind:href="action.url" class="item">
                        <i class="fa-fw" v-bind:class="action.icon"></i>
                    </a>
                </div>
                <div class="table-responsive">
                    <table class="table bc-table">
                        <thead class="header">
                            <tr>
                                <th v-for="header in headers" class="clickable" v-on:click="updateOrder(header)" v-bind:style="{width:getHeaderPercentage()+'%'}">
                                    {{header}} 
                                    <i v-if="order && order.header === header" class="fas fa-fw" :class="order.direction===1?'fa-caret-up':'fa-caret-down'"></i>
                                </th>
                                <th v-if="table_options.dataset.actions.edit.enabled"></th>
                                <th v-if="table_options.dataset.actions.details.enabled"></th>
                                <th v-if="table_options.dataset.actions.remove.enabled"></th>
                            </tr>
                        </thead>
                        <tbody class="body">
                            <tr v-for="row in data">
                                <td v-if="name!==table_options.key || table_options.dataset.showKey" v-for="(value,name) in row">{{value}}</td>
                                <td v-if="table_options.dataset.actions.edit.enabled" class="button">
                                    <a :href="table_options.dataset.actions.edit.url+'/'+row[table_options.key]">
                                        <i class="fas fa-edit fa-fw"></i>
                                    </a>
                                </td>
                                <td v-if="table_options.dataset.actions.details.enabled" class="button">
                                    <a :href="table_options.dataset.actions.details.url+'/'+row[table_options.key]">
                                        <i class="fas fa-file-alt fa-fw"></i>
                                    </a>
                                </td>
                                <td v-if="table_options.dataset.actions.remove.enabled" class="button">
                                    <a :href="table_options.dataset.actions.remove.url+'/'+row[table_options.key]">
                                        <i class="fas fa-trash-alt fa-fw"></i>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div v-if="data.length===0" class="empty-panel">
                    <div class="header">
                        <i class="far fa-info-circle fa-fw fa-2x"></i>
                    </div>
                    <div class="body">
                        There is no data to show.
                    </div>
                </div>
                <div class="table-footer monofont">
                    <div class="table-info-controls d-none d-sm-block">
                        <div class="item">
                            {{pageStart()}} - {{pageEnd()}} of {{count}} items
                            <small><i class="far fa-clock fa-fw item"></i> {{performance}}</small>
                        </div>
                    </div>
                    <div class="pagination-controls">
                        <i class="far fa-chevron-left fa-fw item button clickable" v-if="hasPrevPage" v-on:click="prevPage"></i>
                        <i class="far fa-genderless fa-fw item" v-else></i>
                        <span class="info">{{page}} <span v-if="totalPages>0">of {{totalPages}}</span></span>
                        <i class="far fa-chevron-right fa-fw item button clickable" v-if="hasNextPage" v-on:click="nextPage"></i>
                        <i class="far fa-genderless fa-fw item" v-else></i>
                    </div>
                </div>
            </div>
            <div v-else class="loading-panel">
                <i class="fas fa-sync fa-spin fa-fw fa-2x"></i>
            </div>

        `
});
