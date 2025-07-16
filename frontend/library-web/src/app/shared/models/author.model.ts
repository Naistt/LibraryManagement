import { Book } from "./book.model";

export interface Author {
  id: number;
  name: string;
  bio?: string;
  books?: Book[];
}

export interface CreateAuthor {
  name: string;
  bio?: string;
}

export interface UpdateAuthor {
  name: string;
  bio?: string;
}
