import { Component } from '@angular/core';
import { faCircleXmark, faPlusSquare } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss']
})
export class EmployeesComponent {
  faCircleXmark = faCircleXmark; faPlusSquare = faPlusSquare;
  products1: any[] = [
    { code: '1000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '10000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '2000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '3000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }
    , { code: '4000', name: 'Saravanan Nanjundan', category: 'Staff', quantity: '10', price: 1000 }

  ];
}
