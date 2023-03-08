import { Component } from '@angular/core';
import { faCircleXmark, faPlusSquare } from '@fortawesome/free-regular-svg-icons'
@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss']
})
export class TestComponent {
  faCircleXmark = faCircleXmark; faPlusSquare = faPlusSquare;
  products1: any[] = [
    { code: '1000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '10000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '2000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '3000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '4000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }

  ];
}
