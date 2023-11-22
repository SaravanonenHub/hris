import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';

@Directive({
  selector: '[appDynamicLink]'
})
export class ReadMoreDirective {
    @Input() set appDynamicLink(descriptionData: { description: string, maxLength: number, templateName: string }) {
        const { description, maxLength, templateName } = descriptionData;
        const truncatedText = this.trimDescription(description, maxLength);
    
        const link = this.renderer.createElement('a');
        link.textContent = 'Read More';
        link.classList.add('read-more-link');
        link.setAttribute('data-template-name', templateName);
    
        this.renderer.listen(link, 'click', (event) => this.onReadMoreClicked(event));
    
        const parent = this.el.nativeElement;
        
    
        const textNode = this.renderer.createText(` ${truncatedText}`);
        this.renderer.appendChild(parent, textNode);
        this.renderer.appendChild(parent, link);
      }
    
  constructor(private el: ElementRef, private renderer: Renderer2,private router:Router) {}
  private trimDescription(description: string, maxLength: number): string {
    return description.length > maxLength ? description.slice(0, maxLength) + '...' : description;
  }
  onReadMoreClicked(event: any): void {
    const target = event.target;
    const templateName = target.getAttribute('data-template-name');
    console.log("Evenet listened");
    const routerLink = this.getHrefBasedOnTemplateName(templateName);
    if (routerLink !== '#') {
        this.router.navigate(['/leave']).then(() => console.log('success'),() => {
            console.log(routerLink)
        });
    } else {
        // Handle non-navigable links, if needed
        this.router.navigate(['#']);
    }
    console.log('Read More clicked for template:', templateName);
  }
  getHrefBasedOnTemplateName(templateName: string): string {
    // You can customize the href based on the templateName
    switch (templateName) {
      case 'Leave':
        return '/leave';
      case 'Permission':
        return '#';
      // Add more cases as needed
      default:
        return '#';
    }
  }
}