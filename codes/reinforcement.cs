//-------------------------------------------------------------------
public void DeltaTau(ref MeshEnvironment env, ref List<int> route)
{   
    //Calculate the Delta Tau according to the best cost so far.
    if (best_cost != Double.MaxValue)
    {
        Double current_cost = ExtraTools.GetCost(ref route, ref env);
        delta_tau = best_cost / current_cost;
    }

}
//-------------------------------------------------------------------
public void Reinforcement(ref MeshEnvironment env, ref List<int> route)
{
    for (int i = 0; i < route.Count - 1; i++)
    {   
        //Iteration by each pair of nodes
        int node1_idx = route[i];
        int node2_idx = route[i + 1];

        //Select the associated edge
        int index = env.world[node1_idx].neighboors.IndexOf(node2_idx);
        int edge_idx = env.world[node1_idx].edges[index];

        //Change the delta tau according to the current cost and the best cost so far
        DeltaTau(ref env, ref route);

        //Reinforce the route, delta_tau is a global variable
        env.edges[edge_idx].pheromone_amount += delta_tau;

    }
}