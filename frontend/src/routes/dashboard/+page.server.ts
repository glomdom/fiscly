export const load = async ({ locals, fetch, cookies }) => {
  const token = cookies.get("auth_token");

  const fetchMetrics = async () => {
    const res = await fetch("http://localhost:5096/api/dashboard/aggregate", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) throw new Error("Failed to load metrics");

    return await res.json();
  };

  return {
    user: locals.user,
    streamed: {
      summary: fetchMetrics(),
    },
  };
};
