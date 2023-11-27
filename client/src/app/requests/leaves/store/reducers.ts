// import { createReducer,on } from "@ngrx/store";
// import { LeaveStateInterface } from "../types/leaveStateInterface";
// import * as LeaveActions from './actions'
// export const initialState:LeaveStateInterface={
//     IsSucced:false,
//     Leaves:[],
//     error:null
// };

// export const reducer = createReducer(initialState
//     , on(LeaveActions.getLeaves,(state) => ({...state, IsSucced : false}))
//     , on(LeaveActions.getLeavesSuccess,(state,action) => ({...state, IsSucced : true,Leaves:action.leaves}))
//     );