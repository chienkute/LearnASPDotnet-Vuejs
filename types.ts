export interface User {
  id: number;
  username: string;
  email: string;
  knownAs: string;
  birthday: Date;
  gender: string;
  avatar: string;
  city: string;
}

export interface UserLogin {
  username: string;
  password: string;
}
