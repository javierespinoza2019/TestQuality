import { Injectable } from '@angular/core';

import Swal from 'sweetalert2';
declare var $: any;

@Injectable({
  providedIn: 'root'
})
export class SweetAlertService {

  constructor() { }

  success(title = '', text = '', callback?){
    let options: any = { 
      title: title, 
      text: text, 
      type: 'success',
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
      timer: 1000,
      onBeforeOpen: () => {
        Swal.hideLoading();
      },
      onOpen: () => {
        setTimeout(() => {
          $('.swal2-styled.swal2-confirm').focus();
        }, 100);
      }
    };
    if(callback){
      Swal.fire(options).then((result) => {
        callback(result);
      });
    }else{
      Swal.fire(options);
    }
  }

  error(title = '', text = ''){
      var options = { 
        title: title, 
        text: text, 
        type: 'error', 
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar',
        onBeforeOpen: () => {
          Swal.hideLoading();
        },
        onOpen: () => {
          setTimeout(() => {
            $('.swal2-styled.swal2-confirm').focus();
          }, 100);
        }
      };
    Swal.fire(options);
  }
  
  loading(title = '', text = '') {
    let options: any = {
      title : title,
      text: text,
      allowOutsideClick : false,
      allowEscapeKey : false,
      onOpen: () => {
          Swal.showLoading();
      }
    };
    Swal.fire(options);
  }

  info(title = '', text = '', callback?) {
    let options: any = {
      title: title, 
      text: text, 
      type: 'warning', 
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
      onBeforeOpen: () => {
        Swal.hideLoading();
      },
      onOpen: () => {
        setTimeout(() => {
          $('.swal2-styled.swal2-confirm').focus();
        }, 100);
      }
    };
    if(callback){
      Swal.fire(options).then((result) => {
        callback(result);
      });
    }else{
      Swal.fire(options);
    }
  }

  confirm(title = '', text = '', callbackConfirm?) {
    let options: any = {
      type: 'warning',
      title : title,
      text : text,
      allowOutsideClick : false,
      showCancelButton: true,
      confirmButtonText: 'Aceptar',
      cancelButtonText: 'Cancelar',
      allowEscapeKey : true,
      reverseButtons: true
    };

    Swal.fire(options).then(function (result){
      if(result.value){
        callbackConfirm(result);
      }else{
        callbackConfirm({ value: false });
      }
    });
  }

  close() {
    Swal.close();
  }
}
