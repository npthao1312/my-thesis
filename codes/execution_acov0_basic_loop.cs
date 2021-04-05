public void ExecuteACO(ref MeshEnvironment env)
{
    //Create a list of ants
    List<FinalAntv0> colony = CreateColony();

    //No init, use the stored values // In some version, we use it
    //env.InitPheromones(tau_0);

    //Variables to control evaporation and convergence
    bool almost_one_route = false;
    bool converge = false;

    //Time Variable
    var watch = System.Diagnostics.Stopwatch.StartNew();

    while (!converge)
    {
        foreach (var ant in colony)
        {
            //Try to find a possible route
            List<int> route = ant.FindRoute(ref env);

            //If the ant finds a route
            if (route.Count != 0)
            {

                //Reinforce the route
                Reinforcement(ref env, ref route);

                //Almost one route per episode
                almost_one_route = true;

                //Check and set if the best cost changed
                CheckAndSetBestCost(ref env, ref route);
 
            }

        }

        //Reset the visited nodes
        env.ResetNodes();

        if (almost_one_route)
        {   
            //Evaporation on the environment
            Evaporation(ref env);
            almost_one_route = false;
        }

        converge = CheckConvergence(ref colony, ref env);

        //Episodes
        episode_counter++;

    }

    //Execution Time
    watch.Stop();
    execution_time += watch.ElapsedMilliseconds;
    colony_ants = colony;

}