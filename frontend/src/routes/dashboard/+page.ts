export const load = async ({ fetch, data }) => {
  const {token, user} = data;

  const fetchMetrics = async () => {
    const res = await fetch("http://127.0.0.1:5096/api/dashboard/aggregate", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) throw new Error("Failed to load metrics");

    return await res.json();
  };

  const fetchTransactions = async () => {
    const res = await fetch("http://127.0.0.1:5096/api/transactions", {
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!res.ok) throw new Error("Failed to load transactions");

    return await res.json();
  };

  return {
    user,
    streamed: {
      summary: fetchMetrics(),
      transactions: fetchTransactions(),
    },
  };
};
