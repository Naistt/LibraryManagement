export interface Book {
  id: number;
  title: string;
  summary?: string;
  publicationYear?: number;
  authorId: number;
  genreId: number;
}

export interface CreateBook {
  title: string;
  summary?: string;
  publicationYear?: number;
  authorId: number;
  genreId: number;
}

export interface UpdateBook {
  title: string;
  summary?: string;
  publicationYear?: number;
  authorId: number;
  genreId: number;
}
