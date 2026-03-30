import { fail, redirect, isRedirect } from "@sveltejs/kit";

export const actions = {
  default: async ({ request, fetch }) => {
    const formData = await request.formData();

    const firstName = formData.get("firstName");
    const lastName = formData.get("lastName");
    const email = formData.get("email");
    const password = formData.get("password");

    try {
      const res = await fetch("http://localhost:5096/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ firstName, lastName, email, password }),
      });

      if (!res.ok) {
        return fail(400, { error: "Registration failed. Email might already be in use." });
      }
    } catch (err) {
      if (isRedirect(err)) {
        throw err;
      }

      console.error("Registration fetch error:", err);

      return fail(500, { error: "Cannot connect to the server right now." });
    }

    throw redirect(303, "/login");
  },
};
