import { Component } from '@angular/core';

@Component({
  selector: 'app-open-new-request',
  template: `
    <section>
      <div>
        <h2>Here you have open new request</h2>
        <p>
          <a routerLink="services">
            Open a new request
          </a>
        </p>
      </div>
    </section>
  `,
  styles: [`
    section {
      height: 25vh; /* 25% of the viewport height */
      display: flex;
      justify-content: center;
      align-items: center;
      background-color: rgba(12, 74, 110, 0.8);
      //background-color:linear-gradient(to right, rgba(12, 74, 110, 0.8) 0%, rgba(12, 74, 110, 1));
    }

    div {
      text-align: center;
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
    }`
  ]
})
export class OpenNewRequestComponent {

}
