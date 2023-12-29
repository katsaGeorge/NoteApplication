import {Note} from "./note";

export interface User{
  id : number;
  username: string;
  password: string;
  confPass: string;
  notes : Note[];
}
