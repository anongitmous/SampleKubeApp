using System;
using System.Threading.Tasks;

namespace SampleKubeApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpOperationResponse = await GetItems(args[0], args[1]);
            var body = httpOperationResponse.Body;
            foreach (var item in body.Items)
            {
                if (null != item?.Metadata?.Name)
                {
                    Console.WriteLine($"Pod name: {item.Metadata.Name}");
                }
                else
                {
                    Console.Write("no data");
                }
            }
        }

        static async Task<Microsoft.Rest.HttpOperationResponse<k8s.Models.V1PodList>> GetItems(string kubeCfg, string kubeCtx)
        {
            using var client = new k8s.Kubernetes(k8s.KubernetesClientConfiguration.BuildConfigFromConfigFile(kubeCfg, kubeCtx));
            return await client.ListPodForAllNamespacesWithHttpMessagesAsync(null, null, null, null, null, null,
                null, null, null, null, null).ConfigureAwait(false);
        }
    }
}
