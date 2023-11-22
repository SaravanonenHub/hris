import { createAction, props } from "@ngrx/store";
import { ILeave } from "src/app/domain/models/leave";

export const getLeaves = createAction('[Leaves] Get Leaves');
export const getLeavesSuccess = createAction('[Leaves] Get Leaves success',props<{leaves:ILeave[]}>());