declare global {
  namespace App {
    // interface Error {}
    interface Locals {
      user:
        | {
            id: string;
            firstName: string;
            lastName: string;
            email: string;
          }
        | undefined;
    }
    // interface PageData {}
    // interface PageState {}
    // interface Platform {}
  }
}

export {};
