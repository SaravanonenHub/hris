import { Component } from '@angular/core';
import { IRequestTemplate } from '../domain/models/request';

@Component({
  selector: 'app-request-services',
  template: `
    <div class="center-container">
    <p-dataView #dv [value]="requestServices" layout="grid">
      <ng-template pTemplate="header">
      </ng-template>
      <ng-template let-product pTemplate="gridItem">
          <p-card header="{{product.templateName}}" subheader="Card Subheader" [style]="{ width: '250px' }">       
          <div [appDynamicLink]="{ description: product.description, maxLength: 100, templateName: product.templateName }"></div>
          <!-- <p [innerHTML]="product.description | readMore: 100 :product.templateName"></p> -->
            <ng-template pTemplate="footer">
            <p>
              <a routerLink="leave/leave-add">
              Raise
              </a>
            </p>
                <!-- <p-button label="Save" icon="pi pi-check"></p-button> -->
                <p-button label="Cancel" icon="pi pi-times" styleClass="p-button-secondary" [style]="{ 'margin-left': '.5em' }"></p-button>
            </ng-template>
        </p-card>
      </ng-template>
    </p-dataView>
    
    </div>
  `,
  styles: [`
  .center-container{
    display:flex;
    justify-content:center;
  }
  .read-more-link {
  color: red; /* Change the color as needed */
  text-decoration: underline;
  cursor: pointer;
}
h2, a {
      color: white; /* Set font color to white */
    }
    p {
      cursor: pointer;
      border: 1px solid white; /* Add a white border around the p element */
      padding: 10px; /* Optional: Add some padding to the p element */
    }
    p:hover {
      background-color: rgba(12, 74, 110, 1.5); /* Change background color on hover */
      //color: black; /* Change text color on hover */
    }
  `
  ]
})
export class RequestServicesComponent {

  requestServices:IRequestTemplate[] =[{
   templateName:'Leave' ,
   templatePrefix:'LR',
   description:'The Leave Policy is a confidential document that outlines the policies and procedures for granting employees time off from work. This document outlines the leave categories and the number of days taken for each type. It also provides accrual, carrying over, and vacation time usage information.',
 
  },{
    templateName:'Permission' ,
    templatePrefix:'PR',
    description:'The Leave Policy is a confidential document that outlines the policies and procedures for granting employees time off from work. This document outlines the leave categories and the number of days taken for each type. It also provides accrual, carrying over, and vacation time usage information.',
    
   }]

   constructor(){}
   handleReadMoreClick(templateName:string){
    debugger;
      switch(templateName){
        case 'Leave':
          console.log("Leave Template");
          break;
        default:
          console.log("Default template");
          break;
      }
   }
}
