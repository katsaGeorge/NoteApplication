import {User} from "./user";

export interface Note {
  id?: number;
  subject?: string;
  text? : string;
  userId?: number;
  date?: Date;
  user?: User;

}
