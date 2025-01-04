import { Component, EventEmitter, Injector, Input, Output, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { finalize } from 'rxjs';
import { ModalDirective } from 'src/directive/modal.directive';

@Component({
  selector: 'app-create-or-edit-weather',
  templateUrl: './create-or-edit-weather.component.html',
  styleUrls: ['./create-or-edit-weather.component.scss']
})
export class CreateOrEditWeatherComponent {
  @ViewChild("createOrEditModal", { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  close(): void {
    const closeMessage = 'Modal closed';
    //this.modalRef.close(closeMessage)
  }

  ngOnInit() {
  }
  show(){
    this.modal.show();
  }

  isEdit: Boolean = true;

  // show(id): void {
  //   this.validation();
  //   if (!id) {
  //     this.isEdit = false;
  //     this.asset = new AssetSaveDto();
  //     this.active = true;
  //     this.modal.show();
  //   }
  //   else {
  //     this._assetService.loadById(id).subscribe((result) => {
  //       this.asset.id = result.id;
  //       this.asset.assetName = result.assetName;
  //       this.asset.assetGroupId = result.assetGroupId;
  //       // this.asset.tagCode = result.tagCode;
  //       this.isEdit = true;
  //       this.active = true;
  //       this.modal.show();
  //     }
  //     );
  //   }
  // }


  // save(): void {
  //   this.saving = true;
  //   this._assetService.save(this.asset).pipe(
  //     finalize(() => {
  //       this.saving = false;
  //     })).subscribe(() => {
  //       this.notify.info(this.l(AppConsts.message.saveSuccess));
  //       this.close();
  //       this.modalSave.emit(null);
  //       this.asset = new AssetSaveDto();
  //       this.saving = false;
  //     });
  // }

  // close(): void {
  //   this.active = false;
  //   this.modal.hide();
  // }
}
