<template>
  <div>
    <div class="page-content">
      <div class="row">
        <div class="col-4">
          <b-form-input
            id="input-small1"
            size="sm"
            placeholder="Új ügy Ids"
            v-model="ujUgyIdList"
          ></b-form-input>
        </div>
        <div class="col-4">
          <b-form-input
            id="input-small2"
            size="sm"
            placeholder="Változott ügy Ids"
            v-model="valtozottUgyIdList"
          ></b-form-input>
        </div>
        <div class="col-4">
          <b-form-input
            id="input-small3"
            size="sm"
            placeholder="Megszűnt ügy Ids"
            v-model="megszuntUgyIdList"
          ></b-form-input>
        </div>
      </div>
      <div class="row pt-4">
        <div class="col-12">
          <b-button @click="NotifyUseresOnFegyelmiUgyValtozas()">
            NotifyUseresOnFegyelmiUgyValtozas
          </b-button>
          <b-button @click="ConfirmModal()">
            ConfirmModal
          </b-button>
        </div>
      </div>
      <div class="row pt-4">
        <div class="col-6 offset-3">
          <k-file-upload
            id="esemeny"
            v-model="uploadedFiles"
            projectName="Fenyites"
            @OnUploadError="OnUploadError"
          ></k-file-upload>
          {{ uploadedFiles }}
        </div>
      </div>
      <div class="row pt-4">
        <div id="list-complete-demo" class="demo">
          <button v-on:click="add">Add</button>
          <button v-on:click="remove">Remove</button>
          <transition-group name="list-complete" tag="p">
            <span
              v-for="item in items"
              v-bind:key="item"
              class="list-complete-item"
            >
              {{ item }}
            </span>
          </transition-group>
        </div>
      </div>
      <div class="row pt-4">
        <b-form-input
          id="input-small2"
          size="sm"
          placeholder="Ugyzam"
          v-model="ugySzam"
        ></b-form-input>
        <pre>{{ ugySzamRes }}</pre>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import { apiService, jfkApp } from '../main';
import { NotificationFunctions } from '../functions/notificationFunctions';

export default {
  name: 'teszt',
  data() {
    return {
      ujUgyIdList: '0',
      valtozottUgyIdList: '0',
      megszuntUgyIdList: '138, 139',
      uploadedFiles: [],
      items: [1, 2, 3, 4, 5, 6, 7, 8, 9],
      nextNum: 10,
      ugySzam: 10,
      ugySzamRes: '',
    };
  },
  mounted() {},
  created() {},
  methods: {
    NotifyUseresOnFegyelmiUgyValtozas() {
      apiService.NotifyUseresOnFegyelmiUgyValtozas({
        data: {
          ujUgyIdList: this.StrToArr(this.ujUgyIdList),
          valtozottUgyIdList: this.StrToArr(this.valtozottUgyIdList),
          megszuntUgyIdList: this.StrToArr(this.megszuntUgyIdList),
        },
      });
    },
    StrToArr(str) {
      if (!str.trim()) {
        return [];
      }
      return str
        .trim()
        .split(',')
        .map(m => m.trim());
    },
    OnUploadError({ file, response }) {
      NotificationFunctions.ErrorAlert(
        'Hiba',
        response.message || `${file.name} feltöltése sikertelen.`
      );
    },
    async ConfirmModal() {
      let result = await NotificationFunctions.ConfirmModal({
        title: 'Megerősítés',
      });
      console.log(result);
    },
    randomIndex: function() {
      return Math.floor(Math.random() * this.items.length);
    },
    add: function() {
      this.items.splice(this.randomIndex(), 0, this.nextNum++);
      this.items.splice(this.randomIndex(), 0, this.nextNum++);
    },
    remove: function() {
      this.items.splice(this.randomIndex(), 1);
      this.items.splice(this.randomIndex(), 1);
      this.items.splice(this.randomIndex(), 1);
    },
    async GetOsszefoglalojelentesNyomtatasAdat() {},
  },
  computed: {
    ...mapGetters({}),
  },
  watch: {
    ugySzam: {
      handler: async function(val) {
        let res = await apiService.GetOsszefoglalojelentesNyomtatasAdat({
          fegyelmiUgyId: val,
        });
        console.log(res);
        this.ugySzamRes = JSON.stringify(res, null, 2);
      },
      immediate: true,
    },
  },
  components: {},
};
</script>
