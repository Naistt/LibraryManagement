export interface Genre {
  id: number;
  name: string;
  description?: string;
}

export interface CreateGenre {
  name: string;
  description?: string;
}

export interface UpdateGenre {
  name: string;
  description?: string;
}
