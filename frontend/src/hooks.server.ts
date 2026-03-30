import { redirect } from "@sveltejs/kit";

export const handle = async ({ event, resolve }) => {
  const token = event.cookies.get("auth_token");

  if (event.url.pathname.startsWith("/dashboard")) {
    if (!token) {
      throw redirect(303, "/login");
    }

    const res = await event.fetch("http://localhost:5096/api/auth/me", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) {
      event.cookies.delete("auth_token", { path: "/" });
      
      throw redirect(303, "/login");
    }

    event.locals.user = await res.json();
  }

  if (token && (event.url.pathname === "/login" || event.url.pathname === "/register")) {
    throw redirect(303, "/dashboard");
  }

  return resolve(event);
};
