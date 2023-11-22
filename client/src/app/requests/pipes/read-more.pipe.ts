import { ElementRef, EventEmitter, NgZone, Output, Pipe, PipeTransform, Renderer2 } from "@angular/core";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";
import { Router } from "@angular/router";


@Pipe({
    name:'readMore'
})

export class ReadMorePipe implements PipeTransform{
    constructor(private sanitizer:DomSanitizer, private elementRef:ElementRef
        , private renderer:Renderer2
        ,private router: Router
        ,private zone: NgZone){}
    @Output() readMoreClicked = new EventEmitter<string>();
    transform(description: string,maxWords:number,templateName:string):SafeHtml {
      
        if(description.length > maxWords) {
            const truncatedText = description.substring(0,maxWords) + '...';
            const link = this.renderer.createElement('a');
            const routerLink = this.getHrefBasedOnTemplateName(templateName);
            //link.href = '#';
            //link.setAttribute('routerLink', routerLink);
            //link.setAttribute('routerLink', routerLink);
            link.textContent = 'Read More';
            link.classList.add('read-more-link');
            //this.renderer.listen(link, 'click', () => this.onReadMoreClicked(templateName));
            this.zone.run(() => {
                this.renderer.listen(link, 'click', () => this.onReadMoreClicked(templateName));
            });
            const div = this.renderer.createElement('div');
            div.innerHTML = `${truncatedText} `;
            div.appendChild(link);

            return this.sanitizer.bypassSecurityTrustHtml(div.innerHTML);
        }
        return this.sanitizer.bypassSecurityTrustHtml(description);
            
    }
    getHrefBasedOnTemplateName(templateName: string): string {
        // You can customize the href based on the templateName
        switch (templateName) {
          case 'Leave':
            return '/leave/leave-add';
          case 'Permission':
            return '#';
          // Add more cases as needed
          default:
            return '#';
        }
      }
    onReadMoreClicked(templateName: string) {
        console.log("Evenet listened");
        const routerLink = this.getHrefBasedOnTemplateName(templateName);
        if (routerLink !== '#') {
            this.router.navigate([routerLink]);
        } else {
            // Handle non-navigable links, if needed
            this.readMoreClicked.emit(templateName);
        }
      }
}