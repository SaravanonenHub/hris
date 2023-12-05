import { IEmployee } from "./employee";
import { ILeave } from "./leave";

export interface IRequestTemplate {
    templateName: string,
    templatePrefix: string,
    description: string,
    trimmedDescription?: string,
    isDescriptionLong?: boolean

}

export interface IRequest {
    id: number,
    requestId: string,
    employee: IEmployee,
    typeId: number,
    type: IRequestTemplate,
    description: string,
    status: string,
    cancellationStatus: string,
    requestDate: string,
    currentState: string
}
export type IRequestDetails = {
    id: number,
    requestId: string,
    employee: IEmployee,
    typeId: number,
    type: IRequestTemplate,
    actions: IActionHistory[],
    description: string,
    requestDate: string,
    currentState: string
} & ({
    typeName: 'Leave',
    entries: ILeave,
})

export interface IActionHistory {
    id: number,
    request: IRequest,
    action: string,
    actionDate: Date,
    comment: string,
    actionBy: string,
    summary: string
}