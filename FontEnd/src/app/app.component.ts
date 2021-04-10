import { Component } from '@angular/core';
import { HttpService } from './services/http.service';
import { saveAs } from 'file-saver';
import { SweetAlertService } from './services/SweetAlertService';

declare var $: any;
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Gestor de Archivos';
  public fileToUpload: File = null;
  public fileSelected: String = '';
  public fileList : any = [];
  constructor(private service: HttpService, private swa: SweetAlertService){
    this.getFiles();
  }

  ngOnInt()
  {
    this.resetControls();
  }

  getFiles() {
    this.swa.loading('Administrador de Archivos', 'Obteniendo lista de archivos...');
    this.service.getRequest().subscribe((data) => {      
      this.fileList = data
      this.swa.close();     
    }, (error)=>{
      this.swa.error('Algo ha salido mal, intente nuevamente!', '');
    })
  }

  handleFileInput(files: FileList) {
    if(!files || files.length == 0) return;

    this.fileToUpload = files.item(0);
    this.fileSelected = this.fileToUpload.name;
  }

  loadFile()
  {
    this.swa.loading('Administrador de Archivos', 'Subiendo archivo...');
    this.service.upload(this.fileToUpload).subscribe(data => {
        this.swa.close();
        this.swa.success('Archivo cargado.', '');
        this.closeModal()
        this.getFiles();
      });
  }

  closeModal()
  {    
    $('#fileManagerModal').modal('hide');
    this.resetControls();
  }

  resetControls()
  {
    this.fileToUpload = null;
    this.fileSelected = '';
  }

  downloadFile(fileName)
  {
    this.swa.loading('Administrador de Archivos', 'Descargando archivo...');
    this.service
    .downloadFile(fileName)
    .subscribe((blob) => {       
      saveAs(blob, new Date().getTime() + '_' + fileName);      
      this.swa.close();      
    }, (error)=>{
      this.swa.error('Algo ha salido mal, intente nuevamente!', '');
    })
  }

  deleteFile(fileName)
  {
    this.swa.loading('Administrador de Archivos', 'Eliminando archivo...');
    this.service
    .deleteFile(fileName)
    .subscribe(data => {
      this.swa.close();      
      this.swa.success('Archivo eliminado.', '');   
      this.closeModal();
      this.getFiles(); 
    },(error)=>{
      this.swa.error('Algo ha salido mal, intente nuevamente.', '');
    });
  }
}
