import { fail, redirect } from "@sveltejs/kit";

export const actions = {
  default: async ({ request, cookies }) => {
    const formData = await request.formData();
    const email = formData.get("email");
    const password = formData.get("password");

    const res = await fetch("http://localhost:5096/api/auth/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    if (!res.ok) {
      return fail(400, { error: "Invalid credentials" });
    }

    const { token } = await res.json();

    cookies.set("auth_token", token, {
      path: "/",
      httpOnly: true,
      secure: false,
      sameSite: "lax",
      maxAge: 60 * 60 * 24 * 7,
    });

    throw redirect(303, "/dashboard");
  },
};
