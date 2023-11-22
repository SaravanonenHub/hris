import { Directive, Input, OnInit, TemplateRef, ViewContainerRef, inject } from "@angular/core";
import { IRequestDetails } from "src/app/domain/models/request";

@Directive({
    'selector':'[select]'
})

export class SelectDirective implements OnInit{
    templateRef = inject(TemplateRef);
    vcr = inject(ViewContainerRef);
    @Input() selectFrom!:IRequestDetails;

    async ngOnInit() {
        const data = await this.selectFrom;
        this.vcr.createEmbeddedView(this.templateRef,{
            $implicit:data});
    }
}