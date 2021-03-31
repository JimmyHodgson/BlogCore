const tableComponent = Vue.component('table-component', {
    data: function () {
        return {
            count: 0,
            data: [],
            page: 0,
            headers: [],
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
                pageSize: 10,
                ribbon: []//format {icon:'fas fa-plus', url:'http://domain.com/action'}
            }
        };
    },
    computed: {
        totalPages: function () {
            return parseInt(Math.ceil(this.count / this.options.pageSize));
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
        nextPage() {

        },
        pageEnd() {
            let brutePageEnd = this.table_options.pageSize * this.page;
            return brutePageEnd > this.data.length ? this.data.length : brutePageEnd;
        },
        pageStart() {
            let brutePageStart = (this.page - 1) * this.table_options.pageSize + 1;
            return this.data.length < brutePageStart ? this.data.length : brutePageStart;
        },
        parseOptions() {
            this.table_options = this.expand(this.table_options, this.options);
        },
        prevPage() {

        },
        format(data) {
            //let res = [];
            //for (row in data) {
            //    let current = [];
            //    for (key in row) {
            //        if (format[key]) {
            //            switch (format[key].type) {
            //                case 'url':
            //                    current.push(`<img src="${row[key]}"/>`);
            //                    break;
            //                default:
            //                    current.push(row[key]);
            //                    break;
            //            }
            //        } else {
            //            current.push(row[key]);
            //        }
            //    }
            //    res.push(current);
            //}
            //return res;
            return data;
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
        }
    },
    created() {
        this.parseOptions();
        let start = performance.now();
        let end;
        common.get(`${this.url}?$count=true&$top=${this.table_options.pageSize}`)
            .then(data => {
                end = performance.now();
                this.performance = `${(end - start).toFixed(0)} ms`;
                console.log(data);
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
        },
        ribbon: {
            required: false,
            type: Array
        },
        url: {
            required: true,
            type: String
        }
    },
    template:
        `
            <div v-if="success">
                <div class="table-ribbon">
                    <a v-for="action in ribbon" v-bind:href="action.url" class="item">
                        <i class="fa-fw" v-bind:class="action.icon"></i>
                    </a>
                </div>
                <div class="table-responsive">
                    <table class="table bc-table">
                        <thead class="header">
                            <tr>
                                <th v-for="header in headers">
                                    {{header}}
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
                    <div class="table-info-controls">
                        <div class="item">
                            {{pageStart()}} - {{pageEnd()}} of {{data.length}}
                            <small><i class="far fa-clock fa-fw item"></i> {{performance}}</small>
                        </div>
                    </div>
                    <div class="pagination-controls">
                        <i class="far fa-chevron-left fa-fw item button" v-on:click="prevPage" v-bind:class="hasPrevPage?'clickable':'-disabled'"></i>
                        {{page}}
                        <i class="far fa-chevron-right fa-fw item button" v-on:click="nextPage" v-bind:class="hasNextPage?'clickable':'-disabled'"></i>
                    </div>
                </div>
            </div>
            <div v-else class="loading-panel">
                <i class="fas fa-sync fa-spin fa-fw fa-2x"></i>
            </div>

        `
});
