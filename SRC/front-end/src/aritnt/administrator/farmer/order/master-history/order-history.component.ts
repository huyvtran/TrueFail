import { Component, Injector, ViewChild } from '@angular/core';
import { AppBaseComponent } from 'src/core/basecommon/app-base.component';
import { FarmerOrderService } from '../../farmer-order.service';
import { ResultCode } from 'src/core/constant/AppEnums';
import { DxDataGridComponent } from 'devextreme-angular';
import ArrayStore from 'devextreme/data/array_store'
import { FuncHelper } from 'src/core/helpers/function-helper';
import { FarmerOrderStatus } from '../../farmer-order.service';
import { appUrl } from 'src/aritnt/administrator/app-url';

@Component({
  selector: 'order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent extends AppBaseComponent {
  @ViewChild(DxDataGridComponent) dataGrid: DxDataGridComponent;
  dataSource: ArrayStore;
  selectedRows: number[];

  private from: Date = null;
  private to: Date = null;

  private statuses: FarmerOrderStatus[] = [];

  constructor(
    injector: Injector,
    private orderSvc: FarmerOrderService
  ) {
    super(injector);

    this.from = new Date(Date.now());
    this.to = new Date(Date.now());

    this.orderSvc.getStatuses().subscribe(rs => {
      if(rs.result == ResultCode.Success)
      {
        this.statuses = rs.data;
      }
    });
  }

  loadDatasource(callback: () => void = null) {
    this.orderSvc.getsCompleted(this.from, this.to).subscribe((result) => {
      if (result.result == ResultCode.Success) {

        this.dataSource = new ArrayStore({
          key: "id",
          data: result.data
        });

        if (FuncHelper.isFunction(callback))
          callback();
      }
    });
  }

  private search()
  {
    console.log(this.from);
    console.log(this.to);

    this.loadDatasource(() => {
      this.dataGrid.instance.refresh();
    });
  }

  private infor(orderId: number) {
    this.router.navigate([appUrl.farmerOrderDetail],
      {
        queryParams: {
          type: 'infor',
          id: this.selectedRows[0],
          returnUrl: appUrl.frmerOrderHistory
        }
      });
  }
}
