// import { Injectable } from "@angular/core";
// import { Actions, createEffect, ofType } from "@ngrx/effects";
// import { LeaveService } from "../leave.service";
// import * as LeaveAction from './actions'
// import { map, mergeMap } from "rxjs";
// @Injectable()
// export class LeaveEffect{
   
//     getLeave$ = createEffect(() =>
//     this.actions$.pipe(
//         ofType(LeaveAction.getLeaves), mergeMap(() => {
//             return this.service
//             .getRequests(10)
//             .pipe(map((leaves) => LeaveAction.getLeavesSuccess({leaves})));
//         }))
//     );

//     constructor(private actions$:Actions,private service:LeaveService){}
// }