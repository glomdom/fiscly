import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ locals, cookies }) => {
  return {
    user: locals.user,
    token: cookies.get("auth_token")
  };
};